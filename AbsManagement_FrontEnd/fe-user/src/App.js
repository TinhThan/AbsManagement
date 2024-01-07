import './App.css';
import React, {useEffect,useState} from 'react'
import "@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css";
import "mapbox-gl/dist/mapbox-gl.css";
import Map from './components/map';
import axios from 'axios'
import { Spin } from 'antd';
import { Notification } from './utils/messagebox';

function App() {
  const [spaces,setSpaces] = useState([]); // điểm đặt quảng cáo đã quy hoạch
  const [reports,setReports] = useState(null);
  const [loading,setLoading] = useState([]);

  useEffect(() => {
    getReports();
    getSpaces();
  }, [])
  

  function getSpaces(){
    setLoading(true);
    try {
        axios.get(`${process.env.REACT_APP_BASE_API}api/diemdatquangcao`).then((response) => {
            if(response && response.status === 200)
            {
                setSpaces(response.data)
            }
        }).catch(()=>{
          Notification.Warning("Lấy danh sách điểm đặt thất bại.")
        });
    } catch (error) {
        console.log("error2",error);
    }
    setLoading(false)
  }

  function getReports(){
    setLoading(true);
      try {
          axios.get(`${process.env.REACT_APP_BASE_API}api/baocaovipham`).then((response) => {
              if(response && response.status === 200)
              {
                  setReports(response.data)
              }
          }).catch(()=>{
            Notification.Warning("Lấy danh sách báo cáo vi phạm thất bại.")
          });
      } catch (error) {
        console.log(error);
      }
      setLoading(false)
  }

  return (
    <Spin spinning={loading}>
      {spaces && spaces.length > 0 && reports && <Map spaces={spaces} setReports={setReports} reports={reports}/>}
    </Spin>
  )
}

export default App;
