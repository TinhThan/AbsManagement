import './App.css';
import React, {useEffect,useState} from 'react'
import "@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css";
import "mapbox-gl/dist/mapbox-gl.css";
import Map from './components/map';
import axios from 'axios'

function App() {
  const [spaces,setSpaces] = useState([]); // điểm đặt quảng cáo đã quy hoạch
  const [reports,setReports] = useState([]);

  useEffect(() => {
    getReports();
    getSpaces();
  }, [])
  

  function getSpaces(){
    try {
        axios.get(`${process.env.REACT_APP_BASE_API}api/diemdatquangcao`).then((response) => {
            if(response && response.status === 200)
            {
                setSpaces(response.data)
            }
        });
    } catch (error) {
        console.log("error2",error);
    }
  }

  function getReports(){
      try {
          axios.get(`${process.env.REACT_APP_BASE_API}api/baocaovipham`).then((response) => {
              if(response && response.status === 200)
              {
                  setReports(response.data)
              }
          });
      } catch (error) {
      console.log(error);
      }
  }
  return (
    <>
    {spaces && spaces.length > 0 && <Map spaces={spaces} reports={reports}/>}
    </>
  )
}

export default App;
