import data from '../assets/new-dataHCM.json'

export function getDistrictsAndWards(postcode) {
    const item = data[0].districts.find((obj) => obj.postcode === postcode);
    if(item)
        return item.ward;
    else
        return [];
}

export function getDistrict(district) {
    const item = data[0].districts.find((obj) => district.includes(obj.name) || obj.postcode === district);
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700"
        };
}

export function getWardByDistrict(district, ward) {
    const districtItem = data[0].districts.find((obj) => obj.postcode === district || district.includes(obj.name));
    const item = districtItem?.ward.find((obj) => obj.postcode === ward || ward.includes(obj.name));
    if(item)
        return item;
    else
        return {
            name: "4",
            postcode: "72711"
        };
}