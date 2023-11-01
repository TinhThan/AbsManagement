import { useNavigate } from 'react-router';
import { Button, Result } from 'antd';

export default function NotFoundFeature(): JSX.Element {
  const navigate = useNavigate();
  return (
    <Result
      status='404'
      title='404'
      subTitle={"Page không tồn tại"}
      extra={
        <Button type='primary' onClick={() => navigate('/home')}>
          Trang chủ
        </Button>
      }
    />
  );
}
