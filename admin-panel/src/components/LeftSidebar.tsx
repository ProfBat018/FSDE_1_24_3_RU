import React from 'react';

const LeftSidebar: React.FC = () => {
    return (
        <div>
            <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
                <h1 className="text-2xl font-bold mb-4">Left Sidebar</h1>
                <ul className="space-y-2">
                    <li className="bg-white p-4 rounded shadow hover:bg-gray-200 transition-colors">Item 1</li>
                    <li className="bg-white p-4 rounded shadow hover:bg-gray-200 transition-colors">Item 2</li>
                    <li className="bg-white p-4 rounded shadow hover:bg-gray-200 transition-colors">Item 3</li>
                </ul>
            </div>

        </div>
    );
};
export default LeftSidebar;