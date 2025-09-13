/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_ATTACK_P1 = 3539171364U;
        static const AkUniqueID PLAY_DAMAGE_P1 = 1126428493U;
        static const AkUniqueID PLAY_DEATH_PLAYER_01 = 3657816106U;
        static const AkUniqueID PLAY_DEATH_PORRA_01 = 2420521725U;
        static const AkUniqueID PLAY_GAME_MUSIC = 4141037488U;
        static const AkUniqueID PLAY_HIT_ENE_PORRA_01 = 3770963407U;
        static const AkUniqueID PLAY_HIT_PLAYER_GTR_01 = 2783266843U;
        static const AkUniqueID PLAY_HURT_PORRA_01 = 3228731110U;
        static const AkUniqueID PLAY_WALK_SWITCH = 3559916164U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAME_MUSIC
        {
            static const AkUniqueID GROUP = 258110631U;

            namespace STATE
            {
                static const AkUniqueID LEVEL = 2782712965U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAME_MUSIC

        namespace L1_MUSIC
        {
            static const AkUniqueID GROUP = 540886892U;

            namespace STATE
            {
                static const AkUniqueID ENEMY_PATH = 1507777823U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace L1_MUSIC

    } // namespace STATES

    namespace SWITCHES
    {
        namespace LEVEL_1_SWITCH
        {
            static const AkUniqueID GROUP = 362844960U;

            namespace SWITCH
            {
                static const AkUniqueID L1_ENEMY_PATH_MUSIC = 3017016093U;
            } // namespace SWITCH
        } // namespace LEVEL_1_SWITCH

        namespace WALK_SWITCH_GROUP
        {
            static const AkUniqueID GROUP = 3050293265U;

            namespace SWITCH
            {
                static const AkUniqueID WALK_CONCRETE = 47009900U;
            } // namespace SWITCH
        } // namespace WALK_SWITCH_GROUP

    } // namespace SWITCHES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID AMBIENT = 77978275U;
        static const AkUniqueID MUSIC = 3991942870U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
