export default function SpaceToGeoJson(spaces) {
    const geoJSONFeatures = spaces.map(space => {
        const { diaChi, phuong, quan, viTri, tenLoaiViTri, tenHinhThucQuangCao } = space;
    
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: viTri // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                diaChi,
                phuong,
                quan,
                tenLoaiViTri,
                tenHinhThucQuangCao
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}