import { Routes, Route } from "react-router-dom";
import ShipListPage from "../pages/ShipListPage";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/ships" element={<ShipListPage />} />
    </Routes>
  );
}

export default AppRoutes;