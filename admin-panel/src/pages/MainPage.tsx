import LeftSidebar from "@/components/LeftSidebar";
import Navbar from "@/components/Navbar";
import { useTranslation } from "react-i18next";


const MainPage: React.FC = () => {
    const { t } = useTranslation();

    return (
        <div>
            <Navbar />
            <div className="flex">
                <LeftSidebar />
                <div className="flex-1 p-4">
                    <h1 className="text-2xl font-bold">{t('welcome')}</h1>
                    <p className="mt-4">{t('infoText')}</p>
                </div>
            </div>
        </div>
    );
}


export default MainPage;

