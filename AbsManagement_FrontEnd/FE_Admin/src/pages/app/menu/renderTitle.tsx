import { Space, Typography } from 'antd';
import { StatusMenu } from '.';

interface Props {
  name: string;
  status?: StatusMenu;
}
export const RenderTitle = (props: Props): JSX.Element => {
  const { name, status } = props;
  return (
    <Space align='center'>
      <div className='title-feature'>
        <Typography.Text
          style={{ color: status === StatusMenu.NOTACCESSPERMISSION ? 'rgba(0, 0, 0, 0.25)' : 'white' }}
          ellipsis
        >
          {name}
        </Typography.Text>
      </div>
    </Space>
  );
};
