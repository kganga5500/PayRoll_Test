import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as EmployeeBenifitsStore from '../store/EmployeeBenifits';

// At runtime, Redux will merge together...
type EmployeeBenifitsProps =
    EmployeeBenifitsStore.EmployeeBenifitsState // ... state we've requested from the Redux store
    & typeof EmployeeBenifitsStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters


class EmployeeBenifits extends React.PureComponent<EmployeeBenifitsProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
   // this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">PayRoll Information</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        {this.renderForecastsTable()}
      {/*  {this.renderPagination()}*/}
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
    const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
    this.props.requestEmployeeBenifits(startDateIndex);
  }

  private renderForecastsTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>FirstName</th>
            <th>LastName</th>
            <th>DependentCount</th>

            <th>AnnualDeductionCost</th>
            <th>DeductionCostPerPaycheck</th>
          </tr>
        </thead>
            <tbody>
                {this.props.benifits.map((benift: EmployeeBenifitsStore.EmployeeBenifit) =>
            <tr key={benift.employeeId}>
              <td>{benift.firstName}</td>
                        <td>{benift.lastName}</td>
                        <td>{benift.dependentCount}</td>

              <td >{benift.annualDeductionCost}</td>
              <td>{benift.deductionCostPerPaycheck}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  private renderPagination() {
    const prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    return (
      <div className="d-flex justify-content-between">
        <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${prevStartDateIndex}`}>Previous</Link>
        {this.props.isLoading && <span>Loading...</span>}
        <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${nextStartDateIndex}`}>Next</Link>
      </div>
    );
  }
}

export default connect(
    (state: ApplicationState) => state.employeeBenifits, // Selects which state properties are merged into the component's props
    EmployeeBenifitsStore.actionCreators // Selects which action creators are merged into the component's props
)(EmployeeBenifits as any);
