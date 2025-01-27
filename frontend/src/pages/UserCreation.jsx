import React from "react";
import CreateUserForm from "../components/CreateUserForm";
import Layout from "../layouts/Layout";

export default function UserCreation({ isLoggedIn }) {
  return (
    <Layout current={"Create User"} isLoggedIn={isLoggedIn}>
      <div className="font-bold text-xl sm:text-3xl text-center m-5">
        Create User
      </div>
      <CreateUserForm />
    </Layout>
  );
}
