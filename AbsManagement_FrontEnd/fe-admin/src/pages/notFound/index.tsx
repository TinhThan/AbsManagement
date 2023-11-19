import { Button } from 'antd';
import { redirect } from 'react-router-dom';

export default function NotFoundFeature(): JSX.Element {
  return (
    <Button type='primary' onClick={() => redirect('')}>
    Trang chủ
    </Button>
  );
}
