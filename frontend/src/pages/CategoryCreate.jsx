import React from "react";
import CategoryCreateForm from "../components/CategoryCreateForm";
import Layout from "../layouts/Layout";

/*
 * Just for testing purposes, should be deleted later
 * - checking the functionality for downloading and uploading images and .pdf files
 *
 *	Lea
 */
export default function CategoryCreate({ isLoggedIn }) {
  return (
    <Layout isLoggedIn={isLoggedIn} current={"Create Category"}>
      <div className="font-bold text-xl sm:text-3xl text-center m-5">
        Download and Upload test
      </div>
      {<CategoryCreateForm />}
    </Layout>
  );
}
