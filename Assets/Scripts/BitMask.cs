using UnityEngine;

public static class BitMask
{
    public static int GetBitMask(GridElement[] nearbyGridElements)
    {
        int bitMask = 0;

        if(nearbyGridElements[0] != null)
        {
            if (nearbyGridElements[0].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 1;
            }
        }

        if (nearbyGridElements[1] != null)
        {
            if (nearbyGridElements[1].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 2;
            }
        }

        if (nearbyGridElements[2] != null)
        {
            if (nearbyGridElements[2].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 4;
            }
        }

        if (nearbyGridElements[3] != null)
        {
            if (nearbyGridElements[3].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 8;
            }
        }

        if (nearbyGridElements[4] != null)
        {
            if (nearbyGridElements[4].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 16;
            }
        }

        if (nearbyGridElements[5] != null)
        {
            if (nearbyGridElements[5].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 32;
            }
        }

        if (nearbyGridElements[6] != null)
        {
            if (nearbyGridElements[6].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 64;
            }
        }

        if (nearbyGridElements[7] != null)
        {
            if (nearbyGridElements[7].GetEnabledOrDisabledStatus() == true)
            {
                bitMask += 128;
            }
        }
        return bitMask;

    }
}
