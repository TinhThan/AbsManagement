import { Button, Result } from 'antd';
import { useGoRoute } from '../../hooks/useGoRoute';

export default function NotFoundFeature(): JSX.Element {
  const {goRoute} = useGoRoute();
  return (
    <Result
      status='404'
      title='404'
      subTitle={"Page không tồn tại"}
      extra={
        <Button type='primary' onClick={() => goRoute('Home')}>
          Trang chủ
        </Button>
      }
    />
  );
}
