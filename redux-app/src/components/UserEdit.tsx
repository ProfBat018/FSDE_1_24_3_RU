import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { type RootState, type AppDispatch } from '../store/store'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { format } from 'date-fns'
import { setUserInfo } from '../store/slices/userInfoSlice'

const UserEditForm: React.FC = () => {
    const user = useSelector((state: RootState) => state.userInfo)
    const dispatch = useDispatch<AppDispatch>()
  
    const [name, setName] = useState(user.name)
    const [email, setEmail] = useState(user.email)
    const [profilePictureUrl, setProfilePictureUrl] = useState(user.profilePictureUrl ?? '')
  
    const handleSubmit = (e: React.FormEvent) => {
      e.preventDefault()
      dispatch(setUserInfo({
        id: user.id,
        name,
        email,
        profilePictureUrl: profilePictureUrl || undefined, // Use undefined if empty
        createdAt: user.createdAt,
        updatedAt: new Date() // Update the timestamp to now
      }
      ))
      alert('User updated!')
    }
  
    return (
      <form onSubmit={handleSubmit} className="max-w-md mx-auto p-6 space-y-4 border rounded-xl shadow">
        <h2 className="text-xl font-semibold">Редактировать пользователя</h2>
  
        <div>
          <label className="block mb-1 font-medium">Имя</label>
          <Input value={name} onChange={(e) => setName(e.target.value)} />
        </div>
  
        <div>
          <label className="block mb-1 font-medium">Email</label>
          <Input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
  
        <div>
          <label className="block mb-1 font-medium">Ссылка на аватар</label>
          <Input value={profilePictureUrl} onChange={(e) => setProfilePictureUrl(e.target.value)} />
        </div>
  
        {profilePictureUrl && (
          <div>
            <img src={profilePictureUrl} alt="Аватар" className="h-24 w-24 rounded-full object-cover" />
          </div>
        )}
  
        <div className="text-sm text-gray-500">
          <p>Создан: {format(new Date(user.createdAt), 'dd.MM.yyyy HH:mm')}</p>
          <p>Обновлён: {format(new Date(user.updatedAt), 'dd.MM.yyyy HH:mm')}</p>
        </div>
  
        <Button type="submit">Сохранить</Button>
      </form>
    )
}

export default UserEditForm
