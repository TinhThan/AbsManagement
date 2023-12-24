export default function SpaceToGeoJson(spaces) {
    const geoJSONFeatures = spaces.map(space => {
        const { id, diaChi, phuong, quan, danhSachViTri, tenLoaiViTri, tenHinhThucQuangCao  } = space;
    
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: danhSachViTri // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                diaChi,
                phuong,
                quan,
                tenLoaiViTri,
                tenHinhThucQuangCao,
                id
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}