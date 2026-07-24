import { useEffect, useState } from "react";
import { getCrewMembers, createCrewMember, deleteCrewMember } from "../api/crewApi";

function CrewMemberListPage() {
  const [crewMembers, setCrewMembers] = useState([]);
  const [loading, setLoading] = useState(true);
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
    createCrewMember(formData)
      .then(() => {
        setFormData({ firstName: "", lastName: "", email: "", phoneNumber: "", role: "" });
        fetchCrewMembers();
      })
      .catch((error) => {
        console.error("Error creating crew member:", error);
        alert("Failed to create crew member. Email might already be in use.");
      });
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this crew member?")) return;
    deleteCrewMember(id)
      .then(() => fetchCrewMembers())
      .catch((error) => {
        console.error("Error deleting crew member:", error);
        alert("Failed to delete crew member.");
      });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Crew Members</h1>

      <form onSubmit={handleSubmit}>
        <input name="firstName" placeholder="First Name" value={formData.firstName} onChange={handleChange} required />
        <input name="lastName" placeholder="Last Name" value={formData.lastName} onChange={handleChange} required />
        <input name="email" type="email" placeholder="Email" value={formData.email} onChange={handleChange} required />
        <input name="phoneNumber" placeholder="Phone Number" value={formData.phoneNumber} onChange={handleChange} required />
        <input name="role" placeholder="Role" value={formData.role} onChange={handleChange} required />
        <button type="submit">Add Crew Member</button>
      </form>

      <table>
        <thead>
          <tr>
            <td>ID</td>
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