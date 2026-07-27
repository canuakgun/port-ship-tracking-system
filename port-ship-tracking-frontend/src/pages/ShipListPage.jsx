import { useEffect, useState } from "react";
import { getShips, createShip, updateShip, deleteShip } from "../api/shipApi";
import { Link } from "react-router-dom";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { getErrorMessage } from "../utils/apiError";

function ShipListPage() {
  const [ships, setShips] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingId, setEditingId] = useState(null);
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
        setError("Failed to load ships. Please try again.");
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
    setError(null);
    const dto = { ...formData, yearBuilt: Number(formData.yearBuilt) };

    const request = editingId ? updateShip(editingId, dto) : createShip(dto);

    request
      .then(() => {
        setFormData({ name: "", imo: "", type: "", flag: "", yearBuilt: "" });
        setEditingId(null);
        fetchShips();
      })
      .catch((error) => {
        console.error("Error saving ship:", error);
        setError(getErrorMessage(error, "Failed to save ship."));
      });
  };

  const handleEditClick = (ship) => {
    setFormData({
      name: ship.name,
      imo: ship.imo,
      type: ship.type,
      flag: ship.flag,
      yearBuilt: ship.yearBuilt,
    });
    setEditingId(ship.shipId);
  };

  const handleCancelEdit = () => {
    setFormData({ name: "", imo: "", type: "", flag: "", yearBuilt: "" });
    setEditingId(null);
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this ship?")) return;
    setError(null);
    deleteShip(id)
      .then(() => fetchShips())
      .catch((error) => {
        console.error("Error deleting ship:", error);
        setError(getErrorMessage(error, "Failed to delete ship."));
      });
  };

  if (loading) return <LoadingSpinner />;

  return (
    <div>
      <h1>Ships</h1>

      <ErrorMessage message={error} onDismiss={() => setError(null)} />

      <form onSubmit={handleSubmit} className={editingId ? "editing" : ""}>
        <input
        name="name"
        placeholder="Name"
        value={formData.name}
        onChange={handleChange}
        onInvalid={(e) => e.target.setCustomValidity("Please enter the ship's name.")}
        onInput={(e) => e.target.setCustomValidity("")}
        required
      />
      <input
        name="imo"
        placeholder="IMO (7 digits)"
        value={formData.imo}
        onChange={handleChange}
        maxLength={7}
        pattern="\d{7}"
        title="IMO must be exactly 7 digits"
        onInvalid={(e) => e.target.setCustomValidity("IMO must be exactly 7 digits.")}
        onInput={(e) => e.target.setCustomValidity("")}
        required
      />
      <input
        name="type"
        placeholder="Type"
        value={formData.type}
        onChange={handleChange}
        onInvalid={(e) => e.target.setCustomValidity("Please enter the ship type.")}
        onInput={(e) => e.target.setCustomValidity("")}
        required
      />
      <input
        name="flag"
        placeholder="Flag"
        value={formData.flag}
        onChange={handleChange}
        onInvalid={(e) => e.target.setCustomValidity("Please enter the ship's flag country.")}
        onInput={(e) => e.target.setCustomValidity("")}
        required
      />
      <input
        name="yearBuilt"
        type="number"
        placeholder="Year Built"
        value={formData.yearBuilt}
        onChange={handleChange}
        onInvalid={(e) => e.target.setCustomValidity("Please enter the year the ship was built.")}
        onInput={(e) => e.target.setCustomValidity("")}
        required
      />
      <button type="submit">{editingId ? "Update Ship" : "Add Ship"}</button>
        {editingId && (
          <button type="button" onClick={handleCancelEdit}>
            Cancel
          </button>
        )}
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
                <button onClick={() => handleEditClick(ship)}>Edit</button>
                <button onClick={() => handleDelete(ship.shipId)}>Delete</button>
                 <Link to={`/cargoes/ship/${ship.shipId}`}>
                  <button type="button">View Cargoes</button>
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipListPage;