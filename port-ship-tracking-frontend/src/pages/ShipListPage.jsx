import { useEffect, useState } from "react";
import { getShips, createShip, deleteShip } from "../api/shipApi";

function ShipListPage() {
  const [ships, setShips] = useState([]);
  const [loading, setLoading] = useState(true);
  const [formData, setFormData] = useState({
    name: "",
    imo: "",
    type: "",
    flag: "",
    yearBuilt: "",
  });

  const fetchShips = () => {
    getShips()
      .then((response) => {
        setShips(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching ships:", error);
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchShips();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    createShip({ ...formData, yearBuilt: Number(formData.yearBuilt) })
      .then(() => {
        setFormData({ name: "", imo: "", type: "", flag: "", yearBuilt: "" });
        fetchShips();
      })
      .catch((error) => {
        console.error("Error creating ship:", error);
        alert("Failed to create ship. Check console for details.");
      });
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this ship?")) return;
    deleteShip(id)
      .then(() => fetchShips())
      .catch((error) => {
        console.error("Error deleting ship:", error);
        alert("Failed to delete ship.");
      });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ships</h1>

      <form onSubmit={handleSubmit}>
        <input name="name" placeholder="Name" value={formData.name} onChange={handleChange} required />
        <input name="imo" placeholder="IMO (7 digits)" value={formData.imo} onChange={handleChange} maxLength={7} pattern="\d{7}" title="IMO must be exactly 7 digits" required />
        <input name="type" placeholder="Type" value={formData.type} onChange={handleChange} required />
        <input name="flag" placeholder="Flag" value={formData.flag} onChange={handleChange} required />
        <input name="yearBuilt" type="number" placeholder="Year Built" value={formData.yearBuilt} onChange={handleChange} required />
        <button type="submit">Add Ship</button>
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>IMO</th>
            <th>Type</th>
            <th>Flag</th>
            <th>Year Built</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {ships.map((ship) => (
            <tr key={ship.shipId}>
              <td>{ship.shipId}</td>
              <td>{ship.name}</td>
              <td>{ship.imo}</td>
              <td>{ship.type}</td>
              <td>{ship.flag}</td>
              <td>{ship.yearBuilt}</td>
              <td>
                <button onClick={() => handleDelete(ship.shipId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipListPage;