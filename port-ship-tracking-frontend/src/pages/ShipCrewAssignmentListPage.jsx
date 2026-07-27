import { useEffect, useState } from "react";
import { getAssignments, createAssignment, deleteAssignment } from "../api/assignmentApi";
import { getShips } from "../api/shipApi";
import { getCrewMembers } from "../api/crewApi";
import SearchableSelect from "../components/SearchableSelect";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { getErrorMessage } from "../utils/apiError";

function ShipCrewAssignmentListPage() {
  const [assignments, setAssignments] = useState([]);
  const [ships, setShips] = useState([]);
  const [crewMembers, setCrewMembers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [formData, setFormData] = useState({ shipId: "", crewId: "", assignmentDate: "" });

  const fetchAssignments = () => {
    getAssignments()
      .then((response) => {
        setAssignments(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching assignments:", error);
        setError("Failed to load assignments. Please try again.");
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchAssignments();
    getShips().then((res) => setShips(res.data));
    getCrewMembers().then((res) => setCrewMembers(res.data));
  }, []);

  const shipOptions = ships.map((s) => ({ id: s.shipId, label: s.name }));
  const crewOptions = crewMembers.map((c) => ({ id: c.crewId, label: `${c.firstName} ${c.lastName}` }));

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError(null);
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
        setError(getErrorMessage(error, "Failed to create assignment."));
      });
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this assignment?")) return;
    setError(null);
    deleteAssignment(id)
      .then(() => fetchAssignments())
      .catch((error) => {
        console.error("Error deleting assignment:", error);
        setError(getErrorMessage(error, "Failed to delete assignment."));
      });
  };

  if (loading) return <LoadingSpinner />;

  return (
    <div>
      <h1>Ship Crew Assignments</h1>

      <ErrorMessage message={error} onDismiss={() => setError(null)} />

      <form onSubmit={handleSubmit}>
        <SearchableSelect
          options={shipOptions}
          value={formData.shipId}
          onChange={(id) => setFormData({ ...formData, shipId: id })}
          placeholder="Search Ship..."
        />
        <SearchableSelect
          options={crewOptions}
          value={formData.crewId}
          onChange={(id) => setFormData({ ...formData, crewId: id })}
          placeholder="Search Crew Member..."
        />
        <input name="assignmentDate" type="date" value={formData.assignmentDate} onChange={handleChange}
          onInvalid={(e) => e.target.setCustomValidity("Please select the assignment date.")}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
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