import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCargoesByShipId, createCargo, updateCargo, deleteCargo } from "../api/cargoApi";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { getErrorMessage } from "../utils/apiError";

function CargoListPage() {
  const { shipId } = useParams();
  const [cargoes, setCargoes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState({ description: "", weightTon: "", cargoType: "" });

  const fetchCargoes = () => {
    getCargoesByShipId(shipId)
      .then((response) => {
        setCargoes(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching cargoes:", error);
        setError("Failed to load cargoes. Please try again.");
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchCargoes();
  }, [shipId]);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError(null);
    const dto = {
      ...formData,
      shipId: Number(shipId),
      weightTon: Number(formData.weightTon),
    };
    const request = editingId ? updateCargo(editingId, dto) : createCargo(dto);

    request
      .then(() => {
        setFormData({ description: "", weightTon: "", cargoType: "" });
        setEditingId(null);
        fetchCargoes();
      })
      .catch((error) => {
        console.error("Error saving cargo:", error);
        setError(getErrorMessage(error, "Failed to save cargo."));
      });
  };

  const handleEditClick = (cargo) => {
    setFormData({
      description: cargo.description,
      weightTon: cargo.weightTon,
      cargoType: cargo.cargoType,
    });
    setEditingId(cargo.cargoId);
  };

  const handleCancelEdit = () => {
    setFormData({ description: "", weightTon: "", cargoType: "" });
    setEditingId(null);
  };

  const handleDelete = (id) => {
    if (!window.confirm("Are you sure you want to delete this cargo?")) return;
    setError(null);
    deleteCargo(id)
      .then(() => fetchCargoes())
      .catch((error) => {
        console.error("Error deleting cargo:", error);
        setError(getErrorMessage(error, "Failed to delete cargo."));
      });
  };

  if (loading) return <LoadingSpinner />;

  return (
    <div>
      <h1>Cargoes for Ship #{shipId}</h1>

      <ErrorMessage message={error} onDismiss={() => setError(null)} />

      <form onSubmit={handleSubmit} className={editingId ? "editing" : ""}>
        <input name="description" placeholder="Description" value={formData.description} onChange={handleChange} required />
        <input name="weightTon" type="number" step="0.01" placeholder="Weight (Ton)" value={formData.weightTon} onChange={handleChange} required />
        <input name="cargoType" placeholder="Cargo Type" value={formData.cargoType} onChange={handleChange} required />
        <button type="submit">{editingId ? "Update Cargo" : "Add Cargo"}</button>
        {editingId && <button type="button" onClick={handleCancelEdit}>Cancel</button>}
      </form>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Description</th>
            <th>Weight (Ton)</th>
            <th>Type</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {cargoes.map((cargo) => (
            <tr key={cargo.cargoId}>
              <td>{cargo.cargoId}</td>
              <td>{cargo.description}</td>
              <td>{cargo.weightTon}</td>
              <td>{cargo.cargoType}</td>
              <td>
                <button onClick={() => handleEditClick(cargo)}>Edit</button>
                <button onClick={() => handleDelete(cargo.cargoId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CargoListPage;