import { useEffect, useState } from "react";
import { getVisits, createVisit, updateVisit, deleteVisit } from "../api/shipVisitApi";
import { getShips } from "../api/shipApi";
import { getPorts } from "../api/portApi";
import SearchableSelect from "../components/SearchableSelect";

function ShipVisitListPage() {
  const [visits, setVisits] = useState([]);
  const [ships, setShips] = useState([]);
  const [ports, setPorts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState({
    shipId: "",
    portId: "",
    arrivalDate: "",
    departureDate: "",
    purpose: "",
  });

  const fetchVisits = () => {
    getVisits()
      .then((response) => {
        setVisits(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching visits:", error);
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchVisits();
    getShips().then((res) => setShips(res.data));
    getPorts().then((res) => setPorts(res.data));
  }, []);

  const shipOptions = ships.map((s) => ({ id: s.shipId, label: s.name }));
  const portOptions = ports.map((p) => ({ id: p.portId, label: p.name }));

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const formatDateForInput = (dateString) => dateString ? dateString.split("T")[0] : "";

  const handleSubmit = (e) => {
    e.preventDefault();
    const dto = {
      ...formData,
      shipId: Number(formData.shipId),
      portId: Number(formData.portId),
    };
    const request = editingId ? updateVisit(editingId, dto) : createVisit(dto);

    request
      .then(() => {
        setFormData({ shipId: "", portId: "", arrivalDate: "", departureDate: "", purpose: "" });
        setEditingId(null);
        fetchVisits();
      })
      .catch((error) => {
        console.error("Error saving visit:", error);
        alert("Failed to save visit. Check that arrival date is before departure date.");
      });
  };

  const handleEditClick = (visit) => {
    setFormData({
      shipId: visit.shipId,
      portId: visit.portId,
      arrivalDate: formatDateForInput(visit.arrivalDate),
      departureDate: formatDateForInput(visit.departureDate),
      purpose: visit.purpose,
    });
    setEditingId(visit.visitId);
  };

  const handleCancelEdit = () => {
    setFormData({ shipId: "", portId: "", arrivalDate: "", departureDate: "", purpose: "" });
    setEditingId(null);
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this visit?")) return;
    deleteVisit(id)
      .then(() => fetchVisits())
      .catch((error) => {
        console.error("Error deleting visit:", error);
        alert("Failed to delete visit.");
      });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ship Visits</h1>

      <form onSubmit={handleSubmit}>
        <SearchableSelect
          options={shipOptions}
          value={formData.shipId}
          onChange={(id) => setFormData({ ...formData, shipId: id })}
          placeholder="Search Ship..."
        />
        <SearchableSelect
          options={portOptions}
          value={formData.portId}
          onChange={(id) => setFormData({ ...formData, portId: id })}
          placeholder="Search Port..."
        />
        <input name="arrivalDate" type="date" value={formData.arrivalDate} onChange={handleChange} required />
        <input name="departureDate" type="date" value={formData.departureDate} onChange={handleChange} required />
        <input name="purpose" placeholder="Purpose" value={formData.purpose} onChange={handleChange} required />
        <button type="submit">{editingId ? "Update Visit" : "Add Visit"}</button>
        {editingId && <button type="button" onClick={handleCancelEdit}>Cancel</button>}
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Ship</th>
            <th>Port</th>
            <th>Arrival</th>
            <th>Departure</th>
            <th>Purpose</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {visits.map((visit) => (
            <tr key={visit.visitId}>
              <td>{visit.visitId}</td>
              <td>{visit.shipName}</td>
              <td>{visit.portName}</td>
              <td>{new Date(visit.arrivalDate).toLocaleDateString()}</td>
              <td>{new Date(visit.departureDate).toLocaleDateString()}</td>
              <td>{visit.purpose}</td>
              <td>
                <button onClick={() => handleEditClick(visit)}>Edit</button>
                <button onClick={() => handleDelete(visit.visitId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipVisitListPage;