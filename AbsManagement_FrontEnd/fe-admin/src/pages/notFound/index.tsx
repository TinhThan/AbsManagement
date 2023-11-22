import { Button,Result } from 'antd';
import { redirect, useNavigate } from 'react-router-dom';

export default function NotFoundFeature(): JSX.Element {
  const navigate = useNavigate();
  return (
    // <Button type='primary' onClick={() => navigate('')}>
    // Trang chủ
    // </Button>
    <Result
    status="404"
    title="404"
    subTitle="Sorry, the page you visited does not exist."
    extra={<Button type="primary" onClick={() => navigate('')}>Back Home</Button>}
  />
  );
}
