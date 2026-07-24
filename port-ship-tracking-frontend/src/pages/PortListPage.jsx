import { useEffect, useState } from "react";
import { getPorts, createPort, deletePort } from "../api/portApi";

function PortListPage() {
  const [ports, setPorts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [formData, setFormData] = useState({ name: "", country: "", city: "" });

  const fetchPorts = () => {
    getPorts()
      .then((response) => {
        setPorts(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching ports:", error);
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
    createPort(formData)
      .then(() => {
        setFormData({ name: "", country: "", city: "" });
        fetchPorts();
      })
      .catch((error) => {
        console.error("Error creating port:", error);
        alert("Failed to create port.");
      });
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this port?")) return;
    deletePort(id)
      .then(() => fetchPorts())
      .catch((error) => {
        console.error("Error deleting port:", error);
        alert("Failed to delete port.");
      });
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ports</h1>

      <form onSubmit={handleSubmit}>
        <input name="name" placeholder="Name" value={formData.name} onChange={handleChange} required />
        <input name="country" placeholder="Country" value={formData.country} onChange={handleChange} required />
        <input name="city" placeholder="City" value={formData.city} onChange={handleChange} required />
        <button type="submit">Add Port</button>
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