import { useEffect, useState } from "react";
import { getCrewMembers, createCrewMember, updateCrewMember, deleteCrewMember } from "../api/crewApi";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { getErrorMessage } from "../utils/apiError";

function CrewMemberListPage() {
  const [crewMembers, setCrewMembers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    role: "",
  });

  const fetchCrewMembers = () => {
    getCrewMembers()
      .then((response) => {
        setCrewMembers(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching crew members:", error);
        setError("Failed to load crew members. Please try again.");
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchCrewMembers();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError(null);
    const request = editingId ? updateCrewMember(editingId, formData) : createCrewMember(formData);

    request
      .then(() => {
        setFormData({ firstName: "", lastName: "", email: "", phoneNumber: "", role: "" });
        setEditingId(null);
        fetchCrewMembers();
      })
      .catch((error) => {
        console.error("Error saving crew member:", error);
        setError(getErrorMessage(error, "Failed to save crew member."));
      });
  };

  const handleEditClick = (crew) => {
    setFormData({
      firstName: crew.firstName,
      lastName: crew.lastName,
      email: crew.email,
      phoneNumber: crew.phoneNumber,
      role: crew.role,
    });
    setEditingId(crew.crewId);
  };

  const handleCancelEdit = () => {
    setFormData({ firstName: "", lastName: "", email: "", phoneNumber: "", role: "" });
    setEditingId(null);
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this crew member?")) return;
    setError(null);
    deleteCrewMember(id)
      .then(() => fetchCrewMembers())
      .catch((error) => {
        console.error("Error deleting crew member:", error);
        setError(getErrorMessage(error, "Failed to delete crew member."));
      });
  };

  if (loading) return <LoadingSpinner />;

  return (
    <div>
      <h1>Crew Members</h1>

      <ErrorMessage message={error} onDismiss={() => setError(null)} />

      <form onSubmit={handleSubmit} className={editingId ? "editing" : ""}>
        <input name="firstName" placeholder="First Name" value={formData.firstName} onChange={handleChange}
          onInvalid={(e) => e.target.setCustomValidity("Please enter the first name.")}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
        <input name="lastName" placeholder="Last Name" value={formData.lastName} onChange={handleChange}
          onInvalid={(e) => e.target.setCustomValidity("Please enter the last name.")}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
        <input name="email" type="email" placeholder="Email" value={formData.email} onChange={handleChange}
          onInvalid={(e) => {
            if (e.target.validity.typeMismatch) {
              e.target.setCustomValidity("Please enter a valid email address.");
            } else {
              e.target.setCustomValidity("Please enter an email address.");
            }
          }}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
        <input name="phoneNumber" placeholder="Phone Number" value={formData.phoneNumber} onChange={handleChange}
          onInvalid={(e) => e.target.setCustomValidity("Please enter a phone number.")}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
        <input name="role" placeholder="Role" value={formData.role} onChange={handleChange}
          onInvalid={(e) => e.target.setCustomValidity("Please enter the crew member's role.")}
          onInput={(e) => e.target.setCustomValidity("")}
          required />
        <button type="submit">{editingId ? "Update Crew Member" : "Add Crew Member"}</button>
        {editingId && <button type="button" onClick={handleCancelEdit}>Cancel</button>}
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Role</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {crewMembers.map((crew) => (
            <tr key={crew.crewId}>
              <td>{crew.crewId}</td>
              <td>{crew.firstName}</td>
              <td>{crew.lastName}</td>
              <td>{crew.email}</td>
              <td>{crew.phoneNumber}</td>
              <td>{crew.role}</td>
              <td>
                <button onClick={() => handleEditClick(crew)}>Edit</button>
                <button onClick={() => handleDelete(crew.crewId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CrewMemberListPage;