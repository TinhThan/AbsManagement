import React, { useState } from 'react'
import PropTypes from 'prop-types'
import {Drawer} from 'antd'

function InfoLocation(props) {
    const [open, setOpen] = useState(true);
    return (
        <Drawer destroyOnClose title="Thông tin vị trí" placement="right" onClose={()=>{
            props.onClose()
            setOpen(false)
        }} open={open}>
            <p>{props.lat}</p>
            <p>{props.long}</p>
            <p>{props.address}</p>
            <p>{props.full_address}</p>
        </Drawer>
    )
}

InfoLocation.propTypes = {
    onClose: PropTypes.func,
    setOpen: PropTypes.func,
    lat:PropTypes.number,
    long:PropTypes.number,
    address:PropTypes.string,
    full_address: PropTypes.string,
}

export default InfoLocation
