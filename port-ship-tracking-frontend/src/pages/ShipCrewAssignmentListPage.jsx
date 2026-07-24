import { useEffect, useState } from "react";
import { getAssignments } from "../api/assignmentApi";

function ShipCrewAssignmentListPage() {
  const [assignments, setAssignments] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAssignments()
      .then((response) => {
        setAssignments(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching assignments:", error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ship Crew Assignments</h1>
      <table>
        <thead>
          <tr>
            <th>Ship</th>
            <th>Crew Member</th>
            <th>Assignment Date</th>
          </tr>
        </thead>
        <tbody>
          {assignments.map((assignment) => (
            <tr key={assignment.assignmentId}>
              <td>{assignment.shipName}</td>
              <td>{assignment.crewFullName}</td>
              <td>{new Date(assignment.assignmentDate).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipCrewAssignmentListPage;