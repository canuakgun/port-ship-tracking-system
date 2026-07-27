import { useEffect, useState } from "react";
import { getPorts, createPort, updatePort, deletePort } from "../api/portApi";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { getErrorMessage } from "../utils/apiError";

function PortListPage() {
  const [ports, setPorts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState({ name: "", country: "", city: "" });

  const fetchPorts = () => {
    getPorts()
      .then((response) => {
        setPorts(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching ports:", error);
        setError("Failed to load ports. Please try again.");
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchPorts();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError(null);
    const request = editingId ? updatePort(editingId, formData) : createPort(formData);

    request
      .then(() => {
        setFormData({ name: "", country: "", city: "" });
        setEditingId(null);
        fetchPorts();
      })
      .catch((error) => {
        console.error("Error saving port:", error);
        setError(getErrorMessage(error, "Failed to save port."));
      });
  };

  const handleEditClick = (port) => {
    setFormData({ name: port.name, country: port.country, city: port.city });
    setEditingId(port.portId);
  };

  const handleCancelEdit = () => {
    setFormData({ name: "", country: "", city: "" });
    setEditingId(null);
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this port?")) return;
    setError(null);
    deletePort(id)
      .then(() => fetchPorts())
      .catch((error) => {
        console.error("Error deleting port:", error);
        setError(getErrorMessage(error, "Failed to delete port."));
      });
  };

  if (loading) return <LoadingSpinner />;

  return (
    <div>
      <h1>Ports</h1>

      <ErrorMessage message={error} onDismiss={() => setError(null)} />

        <form onSubmit={handleSubmit} className={editingId ? "editing" : ""}>
    <input
      name="name"
      placeholder="Name"
      value={formData.name}
      onChange={handleChange}
      onInvalid={(e) => e.target.setCustomValidity("Please enter the port's name.")}
      onInput={(e) => e.target.setCustomValidity("")}
      required
    />
    <input
      name="country"
      placeholder="Country"
      value={formData.country}
      onChange={handleChange}
      onInvalid={(e) => e.target.setCustomValidity("Please enter the country.")}
      onInput={(e) => e.target.setCustomValidity("")}
      required
    />
    <input
      name="city"
      placeholder="City"
      value={formData.city}
      onChange={handleChange}
      onInvalid={(e) => e.target.setCustomValidity("Please enter the city.")}
      onInput={(e) => e.target.setCustomValidity("")}
      required
    />
    <button type="submit">{editingId ? "Update Port" : "Add Port"}</button>
    {editingId && <button type="button" onClick={handleCancelEdit}>Cancel</button>}
  </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Country</th>
            <th>City</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {ports.map((port) => (
            <tr key={port.portId}>
              <td>{port.portId}</td>
              <td>{port.name}</td>
              <td>{port.country}</td>
              <td>{port.city}</td>
              <td>
                <button onClick={() => handleEditClick(port)}>Edit</button>
                <button onClick={() => handleDelete(port.portId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default PortListPage;