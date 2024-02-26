import { Outlet } from "react-router-dom";
import { NavBar } from "./navBar/navBar";
import './layout.css';

export function Layout() {
  return (
    <>
      <div className="navbar-container">
        <NavBar />
      </div>
      <div className="content-container">
        <Outlet />
      </div>
    </>
    
  )
}