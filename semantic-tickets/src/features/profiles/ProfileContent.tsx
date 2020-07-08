import React from "react";
import { Tab } from "semantic-ui-react";
import ProfilePhotos from "./ProfilePhotos";
import ProfileFollowings from "./ProfileFollowings";
import ProfileTickets from "./ProfileTickets";

const panes = [
  { menuItem: "Photos", render: () => <ProfilePhotos /> },
  { menuItem: "Tickets", render: () => <ProfileTickets /> },

  {
    menuItem: "Following",
    render: () => <ProfileFollowings />,
  },
  {
    menuItem: "Followers",
    render: () => <ProfileFollowings />,
  },
];

interface IProps {
  setActiveTab: (activeIndex: any) => void;
}

const ProfileContent: React.FC<IProps> = ({ setActiveTab }) => {
  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
      onTabChange={(e, data) => setActiveTab(data.activeIndex)}
      // activeIndex={1}
    />
  );
};

export default ProfileContent;
