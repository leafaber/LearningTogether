import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Select from "../components/Select";
import Layout from "../layouts/Layout";
import SelectionLayout from "../layouts/InputLayout";

export default function AdminPage({ isLoggedIn }) {
  const [categories, setCategories] = useState([]);
  const [email, setEmail] = useState("");
  const [courses, setCourses] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const navigate = useNavigate();
  const onCategoryChange = ({ target: { value } }) => {
    setSelectedCategory(
      value === "0"
        ? null
        : categories.find((cat) => `${cat.categoryId}` === value)
    );
  };

  const onCourseChange = ({ target: { value } }) => {
    setSelectedCourse(
      value === "0"
        ? null
        : courses.find((course) => `${course.course.courseId}` === value)
    );
  };

  const fetchCategories = () => {
    fetch("https://localhost:7002/api/categories", {})
      .then((res) => res.json())
      .then((data) => setCategories(data));
  };

  const fetchCourses = () => {
    fetch("https://localhost:7002/api/courses")
      .then((res) => res.json())
      .then((data) => setCourses(data));
  };
  useEffect(() => {
    /*
     * Fetching the data for categories
     */

    /*
     * Fetch the data for courses
     */
    fetchCategories();
    fetchCourses();
  }, []);

  const handleDeleteCourse = () => {
    fetch(`https://localhost:7002/api/courses`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
      body: `courseName=${selectedCourse.course.courseName}`,
    })
      .then(() => {
        setSelectedCourse("");
        fetchCourses();
      })
      .catch((error) => console.log("An error ocurred:", error));
  };

  const handleDeleteUser = () => {
    fetch(`https://localhost:7002/api/users`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
      body: `email=${email}`,
    })
      .then(() => {
        console.log(`User with email:${email} deleted.`);
        setEmail("");
      })
      .catch((error) => console.log("An error ocurred:", error));
  };

  const handleDeleteCategory = () => {
    fetch(
      `https://localhost:7002/api/categories/${selectedCategory.categoryId}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + window.sessionStorage.getItem("token"),
        },
        mode: "cors",
        body: `categoryName=${selectedCategory.catName}`,
      }
    )
      .then(() => {
        setSelectedCategory(null);
        fetchCategories();
      })
      .catch((error) => console.log("An error ocurred:", error));
  };
  return (
    <Layout current={"Admin"} isLoggedIn={isLoggedIn}>
      <div className="mt-[10%]" />
      <SelectionLayout>
        <Select
          title={"Select Category for deletion"}
          placeholder={"Select category"}
          selectedValue={
            selectedCategory ? `${selectedCategory.categoryId}` : "0"
          }
          options={categories.map((cat) => ({
            value: cat.categoryId,
            label: cat.catName,
          }))}
          onChange={onCategoryChange}
        />
        <div className="flex justify-around gap-x-3">
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 disabled:opacity-50 flex-1"
            onClick={handleDeleteCategory}
            disabled={!selectedCategory}
          >
            Delete
          </button>
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 bg-blue-500 text-white flex-1"
            onClick={() => navigate("/categories")}
          >
            Create
          </button>
        </div>
      </SelectionLayout>

      <div className="mt-[20%]" />

      <SelectionLayout>
        <Select
          title={"Select Course for deletion"}
          placeholder={"Select course"}
          selectedValue={
            selectedCourse ? `${selectedCourse.course.courseId}` : "0"
          }
          options={courses.map(({ course }) => ({
            value: course.courseId,
            label: course.courseName,
          }))}
          onChange={onCourseChange}
        />
        <div className="flex justify-around">
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 disabled:opacity-50 w-2/4"
            onClick={handleDeleteCourse}
            disabled={!selectedCourse}
          >
            Delete
          </button>
        </div>
      </SelectionLayout>

      <div className="mt-[20%]" />
      <SelectionLayout>
        <div className="font-bold text-xl text-center sm:text-3xl m-5">
          Type email for deletion
        </div>
        <div className="mb-3 w-full">
          <input
            className="border-gray-500 border-2 border-radius rounded p-1 w-full"
            type="email"
            name="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="Type e-mail"
          />
        </div>
        <div className="flex justify-around gap-x-3">
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 disabled:opacity-50 flex-1"
            onClick={handleDeleteUser}
            disabled={!email}
          >
            Delete
          </button>
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 bg-blue-500 text-white flex-1"
            onClick={() => navigate("/user-creation")}
          >
            Create
          </button>
        </div>
      </SelectionLayout>
    </Layout>
  );
}
