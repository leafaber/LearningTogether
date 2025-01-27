import React, { useState, useEffect } from "react";

export default function CourseHero(props) {

  function handleRating(num) {
    setRating((prevRating) => prevRating + num);

    fetch(`https://localhost:7002/api/ratings`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8', 'Access-Control-Allow-Origin': '*'
        , 'Authorization': 'Bearer ' + window.sessionStorage.getItem('token')
      },
      mode: 'cors',
      body: `courseName=${courseInfo.courseName}&mark=${num}`
    }).then(res => console.log(res));
  }

  function handleEnroll() {
    console.log("CLICKED")
  }

  const [courseData, setCourseData] = useState([]);
  const [courseInfo, setCourseInfo] = useState([]);
  const [rating, setRating] = useState(0);


  useEffect(() => {

    async function fetchData() {
      const aux = await (await fetch(`https://localhost:7002/api/courses/${props.courseName}`)).json();
      setCourseData(aux);
      setCourseInfo(aux.course);
      setRating(aux.rating);
    }

    fetchData();
  }, []);

  return (
    <div className="relative border-black max-w-[70rem] max-h-[15rem] custom-shadow border">
      <div className="absolute top-[-10px] right-[4px] sm:right-[5px] bg-white border-black custom-shadow-small border px-2 sm:px-12 py-0.5 sm:py-1.5 rounded-bl-lg cursor-pointer" onClick={handleEnroll}>
        <h3 className="sm:text-lg">Enroll</h3>
      </div>
      <div className="flex flex-row">
        <div className="px-5 py-3  flex flex-col w-full sm:w-[90%]">
          <div className="pb-5">
            <h2 className="font-bold py-1 text-lg sm:text-2xl">{courseInfo.courseName}</h2>
            <h2 className="sm:text-xl">{courseData.creatorName}</h2>
          </div>
          <hr className="h-[2px] bg-black my-2" />
          <div className="flex flex-row items-center justify-between sm:my-auto sm:px-10">
            <div className="flex flex-row items-center">
              <img src="../public/images/triangle.png" alt="triangle" className="w-[0.75rem] h-[0.75rem] md:w-[1.2rem] md:h-[1.2rem] mx-1 cursor-pointer" onClick={() => handleRating(1)} />
              <p className="hidden sm:inline-block md:text-2xl px-1">{rating}</p>
              <img src="../public/images/triangle.png" alt="triangle" className="w-[0.75rem] h-[0.75rem] md:w-[1.2rem] md:h-[1.2rem] rotate-180 mx-1 cursor-pointer" onClick={() => handleRating(-1)} />
            </div>

            <div className="flex flex-row items-center">
              <img src="../public/images/hourglass.png" alt="hourglass" className="md:h-[1.5rem] h-[0.75rem] mr-1 md:px-1" />
              <p className="md:text-2xl text-sm">{courseInfo.ects}</p>
            </div>
            <a className="underline md:text-2xl text-sm" href="#">{courseData.categoryName}</a> {/* HERE SHOULD GO courseData.category */}
          </div>
        </div>
        <img src="../public/images/dotGrid.png" alt="dotGrid" className="hidden w-[10%] max-h-[15rem] sm:inline-block border-black border-l" />
      </div>
    </div>
  );
}