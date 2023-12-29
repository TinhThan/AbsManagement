export function SpaceToGeoJson(spaces) {
    const geoJSONFeatures = spaces.map(space => {
        console.log("space",space)
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: space.danhSachViTri // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                id:space.id
            }
        }
    })
    console.log(geoJSONFeatures)
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}

export function ReportToGeoJson(reports) {
    const geoJSONFeatures = reports.map(report => {
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: report.danhSachViTri // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                id:report.id
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}