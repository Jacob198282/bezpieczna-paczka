using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    
    /// This partial class file handles all logic related to the Level Selection view.
    /// It shares the same GameWindow instance with GameWindow.cs and GameWindow.Designer.cs.
    
    public partial class GameWindow : Form
    {
        // Helper list for easy manipulation of level buttons.
        private List<Control> _levelSelectionControls;

        // Note: The logic for wiring up the Click events for Level buttons 
        // must be done here or in the Designer. Let's do it in the Designer 
        // for consistency with the initial setup, but the handlers are here.

        
        /// Logic to switch the UI from the main menu to the level selection screen.
        /// This method is called from picSelectLevel_Click in GameWindow.cs.
        
        private void ShowLevelSelectionView()
        {
            // Initialize controls list on first call (or in constructor if preferred)
            if (_levelSelectionControls == null)
            {
                _levelSelectionControls = new List<Control>
                {
                    picLevel1,
                    picLevel2,
                    picLevel3
                };
            }

            // 1. Hide the initial components (they are accessible because it's the same partial class)
            picLogo.Visible = false;
            picSelectLevel.Visible = false;
            picExitGame.Visible = false; // Optionally, you might want to move this/replace with a Back button.

            // 2. Show the level selection buttons
            foreach (var control in _levelSelectionControls)
            {
                control.Visible = true;
            }
        }

        // --- Level Click Event Handlers ---

        
        /// Handles the Click event for the 'Level 1' picture button.
        
        private void picLevel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Startowanie Poziomu 1 (logika z GameWindow.LevelSelect.cs).");
        }

        
        /// Handles the Click event for the 'Level 2' picture button.
        
        private void picLevel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Startowanie Poziomu 2 (logika z GameWindow.LevelSelect.cs).");
        }

        
        /// Handles the Click event for the 'Level 3' picture button.
        
        private void picLevel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Startowanie Poziomu 3 (logika z GameWindow.LevelSelect.cs).");
        }
    }
}