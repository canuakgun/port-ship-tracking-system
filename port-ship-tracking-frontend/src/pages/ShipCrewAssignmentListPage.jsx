import { useEffect, useState } from "react";
import { getAssignments, createAssignment, deleteAssignment } from "../api/assignmentApi";

function ShipCrewAssignmentListPage() {
  const [assignments, setAssignments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [formData, setFormData] = useState({ shipId: "", crewId: "", assignmentDate: "" });

  const fetchAssignments = () => {
    getAssignments()
      .then((response) => {
        setAssignments(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching assignments:", error);
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchAssignments();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    createAssignment({
      ...formData,
      shipId: Number(formData.shipId),
      crewId: Number(formData.crewId),
    })
      .then(() => {
        setFormData({ shipId: "", crewId: "", assignmentDate: "" });
        fetchAssignments();
      })
      .catch((error) => {
        console.error("Error creating assignment:", error);
        alert("Failed to create assignment. This ship/crew/date combination might already exist.");
      });
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this assignment?")) return;
    deleteAssignment(id)
      .then(() => fetchAssignments())
      .catch((error) => {
        console.error("Error deleting assignment:", error);
        alert("Failed to delete assignment.");
      });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ship Crew Assignments</h1>

      <form onSubmit={handleSubmit}>
        <input name="shipId" type="number" placeholder="Ship ID" value={formData.shipId} onChange={handleChange} required />
        <input name="crewId" type="number" placeholder="Crew ID" value={formData.crewId} onChange={handleChange} required />
        <input name="assignmentDate" type="date" value={formData.assignmentDate} onChange={handleChange} required />
        <button type="submit">Add Assignment</button>
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Ship</th>
            <th>Crew Member</th>
            <th>Assignment Date</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {assignments.map((assignment) => (
            <tr key={assignment.assignmentId}>
              <td>{assignment.assignmentId}</td>
              <td>{assignment.shipName}</td>
              <td>{assignment.crewFullName}</td>
              <td>{new Date(assignment.assignmentDate).toLocaleDateString()}</td>
              <td>
                <button onClick={() => handleDelete(assignment.assignmentId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipCrewAssignmentListPage;