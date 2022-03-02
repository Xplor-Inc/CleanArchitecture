import { useAppSelector } from '../../../ReduxStore/hooks';

export const ProfileImage = () => {
    const profile = useAppSelector(state => state.user);
    return (
        <span>
            {profile.name}
            <img alt={profile.name} width={30} style={{ borderRadius: "50%", width: "30px", border: "1px solid #06bfa6" }} src={`/dynamic/images/${profile.image}`} title={profile.name} />
        </span>
    )
}