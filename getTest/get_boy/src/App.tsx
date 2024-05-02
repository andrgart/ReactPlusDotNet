import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [posts, setPosts] = useState<any[]>([]);

   useEffect(() => {
      fetch('http://localhost:5227/WeatherForecast/test')
         .then((res) => res.json())
         .then((data) => {
            console.log(data);
            setPosts(data);
         })
         .catch((err) => {
            console.log(err.message);
         });
   }, []);

   function TestPOST() {
      fetch('http://localhost:5227/User/register', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          Email : "testEmail",
          FirstName : "testFistName",
          LastName : "testLastName",
          PostAddress : "testPostAddress",
          Password : "testPassword",
        })
      })
   }

  return (
    <div>
      <h1>testPost</h1>
      <button onClick={() => TestPOST()}>test</button>

      {posts.map((post, index) => {
            return(
              <div>
                <h1> {post.date} </h1>
                <p>{ post.temperatureC} </p>
                <p> {post.temperatureF} </p>
              </div>
            )
      })}
    </div>
  );
}

export default App;
