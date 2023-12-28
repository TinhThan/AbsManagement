import { FC} from "react"
import { ForgotPasswordForm } from "../../components/ForgotPasswordForm";

const ForgotPasswordPage: FC = () => {
  return (
    <div className='flex items-center justify-center bg-[#d9edff] min-h-[100vh]'>
      <div
        className='xl:flex lg:block items-center justify-center bg-white rounded-2xl shadow-xl p-[20px]'
        style={{ height: '90%', width: '70%', margin: '0 auto' }}
      >
        <ForgotPasswordForm className="xl:min-w-[50%] lg:w-full p-[20px]" />
        <div className='min-w-[50%]'>
          <iframe src="https://www.google.com/maps/embed?pb=!1m10!1m8!1m3!1d251637.9519623821!2d105.6189045!3d9.779349000000025!3m2!1i1024!2i768!4f13.1!5e0!3m2!1svi!2s!4v1703673434619!5m2!1svi!2s" width="100%" height="450"></iframe>
        </div>
      </div>
    </div>
  )
}

export default ForgotPasswordPage