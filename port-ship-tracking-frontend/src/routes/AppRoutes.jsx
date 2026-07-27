import { Routes, Route, Navigate } from "react-router-dom";
import ShipListPage from "../pages/ShipListPage";
import PortListPage from "../pages/PortListPage";
import ShipVisitListPage from "../pages/ShipVisitListPage";
import CrewMemberListPage from "../pages/CrewMemberListPage";
import ShipCrewAssignmentListPage from "../pages/ShipCrewAssignmentListPage";
import CargoListPage from "../pages/CargoListPage";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/ships" replace />} />
      <Route path="/ships" element={<ShipListPage />} />
      <Route path="/ports" element={<PortListPage />} />
      <Route path="/visits" element={<ShipVisitListPage />} />
      <Route path="/crew" element={<CrewMemberListPage />} />
      <Route path="/assignments" element={<ShipCrewAssignmentListPage />} />
      <Route path="/cargoes/ship/:shipId" element={<CargoListPage />} />
    </Routes>
  );
}

export default AppRoutes;