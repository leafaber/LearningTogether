import React, { useState } from "react";
import InputLayout from "../layouts/InputLayout";
/*
 * If you post a category name that already existed in the db, the picture of that category is changed to a new chosen one
 * If the name isn't already in the db - new category is created
 * This editing method should be changed later - add name editing
 */
// Author: Lea

export default function FileUploader() {
  const [categoryName, setCategoryName] = useState();
  const [selectedFile, setSelectedFile] = useState();

  const handleSubmit = (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("categoryName", categoryName);
    formData.append("file", selectedFile);

    // when posting/creating/editing a category - the category name should also be provided
    fetch("https://localhost:7002/api/categories", {
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
      body: formData,
    })
      .then((response) => response.json())
      .then((result) => {
        console.log("Success:", result);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  };

  return (
    <InputLayout>
      <div className="flex flex-col gap-y-1 items-baseline z-10">
        <form
          className="flex flex-col gap-y-4 justify-center w-full"
          onSubmit={handleSubmit}
        >
          <input
            className="border-gray-500 border-2 border-radius rounded p-1 w-full"
            type="text"
            name="categoryName"
            placeholder="Category name"
            onChange={(event) => setCategoryName(event.target.value)}
            required
          />

          <input
            type="file"
            name="file"
            onChange={(event) => setSelectedFile(event.target.files[0])}
          />

          <button
            className="border-gray-500 border-2 border-radius rounded p-1 bg-blue-500 text-white w-full md:w-2/4 self-center"
            type="submit"
            // for POSTing to work code has to have both 'onSubmit' in the form tag and 'onClick' in button tag
            onClick={handleSubmit}
          >
            Create category
          </button>
        </form>
      </div>
    </InputLayout>
  );
}
