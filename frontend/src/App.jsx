import React, { useState } from "react";
import { Route, Routes, useNavigate } from "react-router-dom";
import Home from "./pages/Home";
import Profile from "./pages/Profile";
import CategoryCreate from "./pages/CategoryCreate";
import "./styles/global.css";
import UserCreation from "./pages/UserCreation";
import AdminPage from "./pages/AdminPage";

/*
 * Contains routes to each page/view
 * path => the url route
 * element => the component from the page folder that should load (aka a new
 * page)
 * 												Mihael
 */
export default function App() {
  const [isAdmin, setIsAdmin] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();
  const handleLogIn = (role) => {
    setIsLoggedIn(true);
    if (role === "Admin") {
      setIsAdmin(true);
      navigate("/admin");
      return;
    }
    navigate("/profile");
  };
  return (
    <Routes>
      <Route
        path="/"
        element={<Home handleLogIn={handleLogIn} isLoggedIn={isLoggedIn} />}
      />
      <Route path="/profile" element={<Profile isLoggedIn={isLoggedIn} />} />
      <Route
        path="/categories"
        element={<CategoryCreate isLoggedIn={isLoggedIn} />}
      />
      <Route path="/admin" element={<AdminPage isLoggedIn={isLoggedIn} />} />
      <Route
        path="/user-creation"
        element={<UserCreation isLoggedIn={isLoggedIn} />}
      />
    </Routes>
  );
}
