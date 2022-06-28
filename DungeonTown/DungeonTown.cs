using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace DungeonTown
{
    /// <summary>
    /// ToDo: 
    /// - Create playable version and upload code to github
    /// - Vendors
    /// - Armors => implement armor use
    /// - Quests (check quest completion in Town Hall) => testing req.
    /// - Refactor code; move things to more logical classes
    /// - Magic/skill system (add Herbalist, Magic Shop, Skill/Magic trainer)
    /// </summary>
    
    
    public partial class DungeonTown : Form
    {
        private Player _player;
        private Building _enteredBuilding;
        private DungeonDelve _dungeonDelve;
        private Dictionary<string, Monster> _currentMonsters;
        private Monster _currentMonster;

        public DungeonTown()
        {
            InitializeComponent();
            _player = new Player("Tim", 15, 15, 0, 1, 0);
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            UpdateLabels();
            UpdateInventoryListInUI();
            UpdateWeaponListInUI();
            UpdatePotionListInUI();
            CBOVisibility(false);
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;
            if (_currentMonster == null)
            {
                _currentMonster = _currentMonsters.ElementAt(0).Value;
                string _currentMonsterName = _currentMonsters.ElementAt(0).Key;
            }

            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);
            _currentMonster.CurrentHitPoints -= damageToMonster;

            rtbMessages.Text += "You hit the " + _currentMonster.Name + " for " + damageToMonster.ToString() + " points." + Environment.NewLine;

            if (_currentMonster.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentMonster.Name + Environment.NewLine;
                _currentMonsters.Remove(_currentMonsters.ElementAt(0).Key);

                _player.ExperiencePoints += _currentMonster.XPReward;
                rtbMessages.Text += "You receive " + _currentMonster.XPReward.ToString() + " experience points" + Environment.NewLine;
                bool LevelUp = _player.LevelUp();
                if (LevelUp)
                {
                    rtbMessages.Text += Environment.NewLine + "Congratulations! You leveled up!" + Environment.NewLine + Environment.NewLine;
                }
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                _player.Gold += _currentMonster.GoldReward;
                rtbMessages.Text += "You receive " + _currentMonster.GoldReward.ToString() + " gold" + Environment.NewLine;

                List<InventoryItem> lootedItems = new List<InventoryItem>();

                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }

                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();
                
                _currentMonster = null;

                if(_currentMonsters.Count == 0)
                {
                    rtbMessages.Text += "You defeated all the monsters!";
                    btnNextRoom.Visible = true;
                    CBOVisibility(false);
                    rtbMessages.Text += Environment.NewLine;
                }
                else
                {
                    rtbMessages.Text += Environment.NewLine;
                    _currentMonster = _currentMonsters.ElementAt(0).Value;
                    string _currentMonsterName = _currentMonsters.ElementAt(0).Key;
                    foreach(KeyValuePair<string, Monster> attackingMonster in _currentMonsters)
                    {
                        MonsterAttack(attackingMonster.Key, attackingMonster.Value);
                    }
                }
            }
            else
            {
                foreach(KeyValuePair<string, Monster> attackingMonster in _currentMonsters)
                {
                    MonsterAttack(attackingMonster.Key, attackingMonster.Value);
                }
                
            }
            
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            Potion potion = (Potion)cboPotions.SelectedItem;

            _player.CurrentHitPoints = (_player.CurrentHitPoints + potion.AmountToHeal);

            if (_player.CurrentHitPoints > _player.MaxHitPoints)
            {
                _player.CurrentHitPoints = _player.MaxHitPoints;
            }

            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details.ID == potion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }

            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;

            foreach (KeyValuePair<string, Monster> attackingMonster in _currentMonsters)
            {
                MonsterAttack(attackingMonster.Key, attackingMonster.Value);
            }
        }

        private void btnDungeon_Click(object sender, EventArgs e)
        {
            rtbMessages.Text += "You head into the danger of the dungeon." + Environment.NewLine;
            grpButtonsBuildings.Visible = false;
            btnHome.Visible = false;
            btnExitBuilding.Visible = true;
            _dungeonDelve = new DungeonDelve(_player.Level);
            btnDungeon.Visible = false;
            btnNextRoom.Visible = true;
            btnNextRoom.PerformClick();
        }

        private void btnNextRoom_Click(object sender, EventArgs e)
        {
            ResourceTick(1);
            switch (_dungeonDelve.NextRoom())
            {
                case 1: // Fancy room, nothing to do here.
                    rtbMessages.Text += World.ReturnDescription() + Environment.NewLine;
                    break;
                case 2: // Free potion(s)
                    Item rewardPotion = _dungeonDelve.PotionReward(_player);
                    rtbMessages.Text += "On a pedestal you see a " + rewardPotion.Name + "." + 
                        Environment.NewLine + "You grab it." + Environment.NewLine;
                    _player.AddItemToInventory(rewardPotion);
                    UpdatePotionListInUI();
                    UpdateInventoryListInUI();
                    break;
                case 3: // Grand treasure
                    _player.Gold += RandomNumberGenerator.NumberBetween(20, 50);
                    _player.Lumber += RandomNumberGenerator.NumberBetween(10, 25);
                    _player.Stone += RandomNumberGenerator.NumberBetween(10, 25);
                    _player.Iron += RandomNumberGenerator.NumberBetween(10, 25);
                    rtbMessages.Text += "Congratulations! You found a grand treasure room!" + Environment.NewLine +
                        "All your resources have increased!" + Environment.NewLine;
                    UpdateLabels();
                    break;
                case 4: // Regular treasure
                    _player.Gold += RandomNumberGenerator.NumberBetween(5, 15);
                    rtbMessages.Text += "You find a small pile of gold!" + Environment.NewLine;
                    lblGold.Text = _player.Gold.ToString();
                    break;
                case 5: // Monster(s)!
                    btnNextRoom.Visible = false;
                    CBOVisibility(true);
                    int[] monsterStorage = _dungeonDelve.MonsterIDPicker(_player.Level);
                    int monsterID = monsterStorage[0];
                    int monsterAmount = monsterStorage[1];
                    rtbMessages.Text += "You see a " + World.MonsterByID(monsterID).Name + ". ";
                    if(monsterAmount > 1) { rtbMessages.Text += "And another one. "; }
                    if(monsterAmount > 2) { rtbMessages.Text += "And another. "; }
                    rtbMessages.Text += "Get ready for battle." + Environment.NewLine;
                    _currentMonsters = _dungeonDelve.CreateMonsters(monsterID, monsterAmount);
                    break;
            }
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            MiscBuilding upgradeBuilding = (MiscBuilding)_enteredBuilding.GetBuiltBuilding(_enteredBuilding, _player).Details;
            if (upgradeBuilding.Upgrade(_player))
            {
                rtbMessages.Text += "You've succesfully upgraded the " + upgradeBuilding.Name + "." + Environment.NewLine;
                rtbMessages.Text += "It will now output twice as much!" + Environment.NewLine;
                UpdateLabels();
                if (upgradeBuilding.UpgradeLvl > 4)
                {
                    btnUpgrade.Visible = false;
                }
            }
            else
            {
                rtbMessages.Text += "Insufficient resources to upgrade." + Environment.NewLine;
                rtbMessages.Text += "It will cost " + upgradeBuilding.UpgradeCostString() + " to upgrade the " + upgradeBuilding.Name + "." + Environment.NewLine;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            rtbMessages.Text += "You head for the safety of home." + Environment.NewLine;
            if (_player.CurrentHitPoints < _player.MaxHitPoints)
            {
                rtbMessages.Text += "You go to sleep and wake up fully rested." + Environment.NewLine;
                _player.CurrentHitPoints = _player.MaxHitPoints;
                ResourceTick(3);
                UpdateLabels();
            }
        }

        // Buttons for entering the buildings.
        private void btnLumberMill_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_LUMBER_MILL));
        }

        private void btnQuarry_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_QUARRY));
        }

        private void btnMine_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_MINE));
        }

        private void btnTrapper_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_TRAPPER));
        }

        private void btnOutfitter_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_OUTFITTER));
        }

        private void btnBlacksmith_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_BLACKSMITH));
        }

        private void btnTownHall_Click(object sender, EventArgs e)
        {
            buttonBuilding(World.BuildingByID(World.BUILDING_ID_TOWN_HALL));
        }

        // 'Maintenance' methods
        private Control FindFirstControl(string name)
        {
            Control[] controls = this.Controls.Find(name, true);
            return controls[0];
        }

        private void UpdateLabels()
        {
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblLumber.Text = _player.Lumber.ToString();
            lblStone.Text = _player.Stone.ToString();
            lblIron.Text = _player.Iron.ToString();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }
            
            if (weapons.Count == 0)
            {
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<Potion> healingPotions = new List<Potion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Potion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((Potion)inventoryItem.Details);
                    }
                }
            }
            
            if (healingPotions.Count == 0)
            {
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void CBOVisibility(bool yesNo)
        {
            if (yesNo)
            {
                lblSelectAction.Visible = true;
                cboPotions.Visible = true;
                btnUsePotion.Visible = true;
                cboWeapons.Visible = true;
                btnUseWeapon.Visible = true;
            }
            else
            {
                lblSelectAction.Visible = false;
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
        }

        private void ResourceTick(int amountOfTicks)
        {
            List<MiscBuilding> miscBuildings = new List<MiscBuilding>();
            
            foreach (BuiltBuilding builtBuilding in _player.BuildingsBuilt)
            {
                if(builtBuilding.Details is MiscBuilding)
                {
                    miscBuildings.Add((MiscBuilding)builtBuilding.Details);
                }
            }
            foreach(MiscBuilding miscBuilding in miscBuildings)
            {
                if (miscBuilding.Type == "Produces Resources")
                {
                    switch (miscBuilding.Name)
                    {
                        case "Lumber Mill":
                            _player.Lumber += miscBuilding.Amount * amountOfTicks;
                            break;
                        case "Quarry":
                            _player.Stone += miscBuilding.Amount * amountOfTicks;
                            break;
                        case "Mine":
                            _player.Iron += miscBuilding.Amount * amountOfTicks;
                            break;
                        default:
                            break;
                    }
                }
            }
            UpdateLabels();
        }

        // Building methods (building a building, entering, exiting, etc.)
        private void buttonBuilding(Building building)
        {
            bool buildingHasBeenBuilt = false;

            foreach (BuiltBuilding builtBuilding in _player.BuildingsBuilt)
            {
                if (builtBuilding.Details.ID == building.ID)
                {
                    buildingHasBeenBuilt = true;
                    if (builtBuilding.buildingUpgradeable(builtBuilding))
                    {
                        btnUpgrade.Visible = true;
                    }
                    break;
                }
            }

            if (buildingHasBeenBuilt == false)
            {
                enterBuilding(false, building);
                rtbMessages.Text += "You enter an empty plot of land. This would be a good place to build a " + building.Name + "." +
                    Environment.NewLine + "Would you like to build the " + building.Name + " for the cost of " + building.ResourceCost() + "?" + Environment.NewLine;
            }
            else
            {
                rtbMessages.Text += "You enter the " + building.Name + "." + Environment.NewLine;
                enterBuilding(true, building);
            }
        }

        private void enterBuilding(bool built, Building enteredBuilding)
        {
            _enteredBuilding = enteredBuilding;
            btnDungeon.Visible = false;
            btnHome.Visible = false;
            btnExitBuilding.Visible = true;
            grpButtonsBuildings.Visible = false;
            if (!built)
            {
                btnBuildBuilding.Visible = true;
            }
            else
            {
                if(_enteredBuilding.ID == 7)
                {
                    QuestHandler();
                }
            }
        }

        private void btnExitBuilding_Click(object sender, EventArgs e)
        {
            _enteredBuilding = null;
            btnDungeon.Visible = true;
            btnHome.Visible = true;
            grpButtonsBuildings.Visible = true;
            if (btnBuildBuilding.Visible) { btnBuildBuilding.Visible = false; }
            if (btnUpgrade.Visible) { btnUpgrade.Visible = false; }
            if (btnNextRoom.Visible || btnUseWeapon.Visible) { rtbMessages.Text += "You return to the safety of town." + Environment.NewLine; }
            else { rtbMessages.Text += "You return outside." + Environment.NewLine; }
            btnExitBuilding.Visible = false;
            btnNextRoom.Visible = false;
        }

        private void btnBuildBuilding_Click(object sender, EventArgs e)
        {
            if (_player.Gold >= _enteredBuilding.GoldCost && _player.Lumber >= _enteredBuilding.WoodCost &&
                _player.Stone >= _enteredBuilding.StoneCost && _player.Iron >= _enteredBuilding.StoneCost)
            {
                _player.Gold -= _enteredBuilding.GoldCost;
                _player.Lumber -= _enteredBuilding.WoodCost;
                _player.Stone -= _enteredBuilding.StoneCost;
                _player.Iron -= _enteredBuilding.IronCost;
                UpdateLabels();
                BuiltBuilding newBuilding = new BuiltBuilding(_enteredBuilding);
                _player.BuildingsBuilt.Add(newBuilding);
                btnBuildBuilding.Visible = false;
                if (newBuilding.buildingUpgradeable(newBuilding))
                {
                    btnUpgrade.Visible = true;
                }
                rtbMessages.Text += "You've succesfully built the " + _enteredBuilding.Name + ". Congratulations!" + Environment.NewLine;

                if(_enteredBuilding.ID == 4)
                {
                    _player.AddItemToInventory(World.ItemByID(2)); // Quarry has been built, player gets stone sword
                    rtbMessages.Text += "Congratulations, you have been awarded the stone sword!";
                }
                else if(_enteredBuilding.ID == 5)
                {
                    _player.AddItemToInventory(World.ItemByID(3)); // Mine has been built, player gets bronze sword
                    rtbMessages.Text += "Congratulations, you have been awarded the bronze sword!";
                }
                else if (_enteredBuilding.ID == 2)
                {
                    _player.AddItemToInventory(World.ItemByID(4)); // Blacksmith has been built, player gets iron sword
                    rtbMessages.Text += "Congratulations, you have been awarded the iron sword!";
                }

                // Checking if requirements for other buildings are met.
                foreach (Building building in World.Buildings)
                {
                    int requirement = World.BuildingByID(building.Requirements).ID;
                    foreach (BuiltBuilding builtBuilding in _player.BuildingsBuilt)
                    {
                        if (requirement == builtBuilding.Details.ID)
                        {
                            Array buttons = this.Controls.Find(building.CorrespondingButton, true);
                            foreach (Button button in buttons)
                            {
                                button.Visible = true;
                            }
                        }
                    }
                }
            }
            else
            {
                rtbMessages.Text += "Not enough resources to build the " + _enteredBuilding.Name + "." + Environment.NewLine;
            }
        }

        // Misc methods
        private void QuestHandler()
        {
            foreach(Quest quest in World.Quests)
            {
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(quest);
                bool playerAlreadyHasQuest = _player.HasThisQuest(quest);
                if (!playerAlreadyCompletedQuest && !playerAlreadyHasQuest)
                {
                    rtbMessages.Text += Environment.NewLine + "You receive the " + quest.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += quest.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    _player.Quests.Add(new PlayerQuest(quest));
                    break;
                }
                else if (!playerAlreadyCompletedQuest)
                {
                    bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(quest);

                    if (playerHasAllItemsToCompleteQuest)
                    {
                        rtbMessages.Text += Environment.NewLine;
                        rtbMessages.Text += "You complete the '" + quest.Name + "' quest." + Environment.NewLine;

                        _player.RemoveQuestCompletionItems(quest);

                        rtbMessages.Text += "You receive: " + Environment.NewLine;
                        rtbMessages.Text += quest.XPReward.ToString() + " experience points" + Environment.NewLine;
                        rtbMessages.Text += quest.GoldReward.ToString() + " gold" + Environment.NewLine;
                        rtbMessages.Text += quest.RewardItem.Name + Environment.NewLine;
                        rtbMessages.Text += Environment.NewLine;

                        _player.ExperiencePoints += quest.XPReward;
                        _player.Gold += quest.GoldReward;

                        _player.AddItemToInventory(quest.RewardItem);

                        _player.MarkQuestCompleted(quest);
                    }
                }
            }
        }

        private void MonsterAttack(string monsterName, Monster monster)
        {
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, monster.MaximumDamage);
            rtbMessages.Text += monsterName + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
            _player.CurrentHitPoints -= damageToPlayer;
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            if (_player.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += monsterName + " killed you." + Environment.NewLine;
                rtbMessages.Text += monsterName + " loots you. You lost some gold." + Environment.NewLine;
                _player.Gold -= (int)(_player.Gold * 0.15);
                _player.CurrentHitPoints = 0;
                _currentMonsters = null;
                UpdateLabels();
                CBOVisibility(false);
                btnExitBuilding.PerformClick();
            }
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            _player.Gold += 9999;
            _player.Lumber += 9999;
            _player.Stone += 9999;
            _player.Iron += 9999;
            _player.CurrentHitPoints -= 1;
            _player.AddItemToInventory(World.ItemByID(102)); // Add basic healing potion
            _player.AddItemToInventory(World.ItemByID(10)); // Add test sword
            UpdateLabels();
            UpdateWeaponListInUI();
            UpdatePotionListInUI();
            UpdateInventoryListInUI();
        }

        private void rtbMessages_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void rtbLocation_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
    }
}
