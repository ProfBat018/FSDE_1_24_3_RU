import type React from "react";
import { RxAvatar } from "react-icons/rx";
import styles from "@/components/Navbar.module.css";
import { useTranslation } from "react-i18next";

const Navbar: React.FC = () => {
  const { i18n } = useTranslation();

  const handleLanguageChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedLanguage = event.target.value;
    i18n.changeLanguage(selectedLanguage);
  };

  return (
    <div className="flex justify-end items-center p-4 bg-gray-800 text-white">
      <div className="flex items-center space-x-6">
        <div className={styles.languageSelector}>
          <label htmlFor="langSelect" className="mr-2">
            Lang
          </label>
          <select
            id="langSelect"
            className={styles.languageDropdown}
            onChange={handleLanguageChange}
            value={i18n.language} 
          >
            <option value="en">EN</option>
            <option value="ru">RU</option>
            <option value="az">AZ</option>
          </select>
        </div>

        <RxAvatar className="w-8 h-8" />
      </div>
    </div>
  );
};

export default Navbar;
