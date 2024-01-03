export function SpaceToGeoJson(spaces,tinhTrang) {
    const geoJSONFeatures = spaces.filter(space=>space.idTinhTrang === tinhTrang).map(space => {
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: space.danhSachViTri // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                id:space.id,
                idTinhTrang: space.idTinhTrang
            }
        }
    })
    console.log(geoJSONFeatures)
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}

export function ReportToGeoJson(spaces, reports) {
    const geoJSONFeatures = reports.map(report => {
        const space = spaces.find(t=>t.id === report.idDiemDatQuangCao);
        let coordinates = report.danhSachViTri;
        console.log("coordinates",coordinates)
        if(space)
        {
            coordinates = [report.danhSachViTri[0] -  0.00002696274,report.danhSachViTri[1]]
        }
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: coordinates // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                id: report.id,
                idTinhTrang: report.idTinhTrang
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}