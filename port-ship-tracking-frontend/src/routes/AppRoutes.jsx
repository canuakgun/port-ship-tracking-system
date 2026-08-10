import { Routes, Route, Navigate } from "react-router-dom";
import ShipListPage from "../pages/ShipListPage";
import PortListPage from "../pages/PortListPage";
import ShipVisitListPage from "../pages/ShipVisitListPage";
import CrewMemberListPage from "../pages/CrewMemberListPage";
import ShipCrewAssignmentListPage from "../pages/ShipCrewAssignmentListPage";
import CargoListPage from "../pages/CargoListPage";
import LoginPage from "../pages/LoginPage";
import ProtectedRoute from "../components/ProtectedRoute";
import LandingPage from "../pages/LandingPage";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<LandingPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/ships" element={<ProtectedRoute><ShipListPage /></ProtectedRoute>} />
      <Route path="/ports" element={<ProtectedRoute><PortListPage /></ProtectedRoute>} />
      <Route path="/visits" element={<ProtectedRoute><ShipVisitListPage /></ProtectedRoute>} />
      <Route path="/crew" element={<ProtectedRoute><CrewMemberListPage /></ProtectedRoute>} />
      <Route path="/assignments" element={<ProtectedRoute><ShipCrewAssignmentListPage /></ProtectedRoute>} />
      <Route path="/cargoes/ship/:shipId" element={<ProtectedRoute><CargoListPage /></ProtectedRoute>} />
    </Routes>
  );
}

export default AppRoutes;