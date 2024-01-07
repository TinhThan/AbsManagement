export function SpaceToGeoJson(spaces,daQuyHoach) {
    const geoJSONFeatures = spaces.filter(space=>daQuyHoach ? space.idTinhTrang === 'DaQuyHoach' : space.idTinhTrang !== 'DaQuyHoach' ).map(space => {
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
        let coordinates = report.danhSachViTri;
        console.log("coordinates",coordinates)
        coordinates = [report.danhSachViTri[0] -  (report.idBangQuangCao | report.idDiemDatQuangCao ?  0.00003696274 : 0),report.danhSachViTri[1]]
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