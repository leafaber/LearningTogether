import React from "react";
import Footer from "../components/Footer";
import MobileMenu from "../components/MobileMenu";
import NavBar from "../components/NavBar";

export default function Layout({ children, current, isLoggedIn }) {
  return (
    <div className="min-h-screen flex flex-col justify-items-stretch">
      <NavBar isLoggedIn={isLoggedIn} />

      <div className="flex flex-col flex-1 px-10 sm:px-[20%] justify-around pb-10">
        {children}
      </div>
      <div className="visible min-[782px]:hidden flex justify-center">
        <MobileMenu current={current} isLoggedIn={isLoggedIn} />
      </div>
      <Footer />
    </div>
  );
}
