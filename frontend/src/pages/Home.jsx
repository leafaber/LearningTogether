import React, { useState, useEffect } from "react";
import Hero from "../components/Hero";
import Footer from "../components/Footer";
import CardHolder from "../components/CardHolder";
import NavBar from "../components/NavBar";
import MobileMenu from "../components/MobileMenu";
import Layout from "../layouts/Layout";
/**********************************************
 | Rev: 1.0 - added the page, just for testing purposes | Author: Mihael | 
 | Rev: 1.3 - added another CardHolder
 |          - moved data fetching from CardHolder       | Author: Mihael |

 **********************************************/
export default function Home({ handleLogIn, isLoggedIn }) {
  const [categories, setCategories] = useState([]);
  const [courses, setCourses] = useState([]);

  /*
   * Fetching the necessary data to be sent to lower level components
   */
  useEffect(() => {
    /*
     * Fetching the data for categories
     */
    fetch("https://localhost:7002/api/categories")
      .then((res) => res.json())
      .then((data) => setCategories(data));

    /*
     * Fetch the data for courses
     */
    fetch("https://localhost:7002/api/courses")
      .then((res) => res.json())
      .then((data) => setCourses(data));
  }, []);

  return (
    <Layout current="Home" isLoggedIn={isLoggedIn}>
      <div className="mt-[26vh] sm:mt-[23%]" />
      {!isLoggedIn && (
        <>
          {" "}
          <Hero handleLogIn={handleLogIn} />
          <div className="flex justify-center items-center">
            <span className="h-96 border-r-[3px] border-dotted border-black"></span>
          </div>
        </>
      )}
      <CardHolder
        title="Our courses"
        data={courses.slice(0, 20)}
        type="course"
      />
      <div className="flex justify-center items-center">
        <span className="h-96 border-r-[3px] border-dotted border-black"></span>
      </div>
      {/* sliced to 20 courses, because it would be unreasonable to load all 100+ courses */}
      <CardHolder
        title="Choose the category for you!"
        data={categories}
        type="category"
      />
      <div className="flex justify-center items-center">
        <span className="h-96 border-r-[3px] border-dotted border-black"></span>
      </div>
    </Layout>
  );
}
