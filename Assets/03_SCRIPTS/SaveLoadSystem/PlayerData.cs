using System.Collections.Generic;

namespace HexaSort.SaveLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int currentUnlockedLevel; // Level cao nhất đã mở khóa
        public List<LevelSaveData> levelProgress; // Danh sách điểm số từng level

        // Constructor mặc định cho người chơi mới
        public PlayerData()
        {
            currentUnlockedLevel = 1;
            levelProgress = new List<LevelSaveData>();
        }

        // Hàm tiện ích để cập nhật dữ liệu khi thắng level
        public void CompleteLevel(int levelId, int stars = 0)
        {
            // 1. Mở khóa level tiếp theo
            if (levelId >= currentUnlockedLevel)
            {
                currentUnlockedLevel = levelId + 1;
            }

            // 2. Lưu thông tin level này (nếu chưa có thì thêm mới)
            LevelSaveData data = levelProgress.Find(x => x.levelId == levelId);
            if (data == null)
            {
                data = new LevelSaveData { levelId = levelId, stars = stars };
                levelProgress.Add(data);
            }
            else
            {
                // Chỉ cập nhật nếu đạt sao cao hơn (tùy logic game của bạn)
                if (stars > data.stars) data.stars = stars;
            }
        }
    }
}