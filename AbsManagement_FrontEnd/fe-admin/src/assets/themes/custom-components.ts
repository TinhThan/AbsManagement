import { Button, Input } from "antd"
import styled from "styled-components"

const InputStyle = styled(Input)`
  min-height: 40px;
`

const InputPasswordStyle = styled(Input.Password)`
  min-height: 40px;
`

const BtnSubmitStyle = styled(Button)`
  width: 100%;
  min-height: 38px;
  background-color: #1890ff;
  margin-top: 2rem;
`

const CustomComponent = {
  InputStyle,
  InputPasswordStyle,
  BtnSubmitStyle
}

export default CustomComponent