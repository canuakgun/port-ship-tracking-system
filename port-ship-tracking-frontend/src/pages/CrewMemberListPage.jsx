import { useEffect, useState } from "react";
import { getCrewMembers } from "../api/crewApi";

function CrewMemberListPage() {
  const [crewMembers, setCrewMembers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getCrewMembers()
      .then((response) => {
        setCrewMembers(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching crew members:", error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Crew Members</h1>
      <table>
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Role</th>
          </tr>
        </thead>
        <tbody>
          {crewMembers.map((crew) => (
            <tr key={crew.crewId}>
              <td>{crew.firstName}</td>
              <td>{crew.lastName}</td>
              <td>{crew.email}</td>
              <td>{crew.phoneNumber}</td>
              <td>{crew.role}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CrewMemberListPage;