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
        static const AkUniqueID PLAY_AMBIENT = 1562304622U;
        static const AkUniqueID PLAY_ARMOIRE_OUVRIR = 1778446475U;
        static const AkUniqueID PLAY_HORLOGE = 1086738306U;
        static const AkUniqueID PLAY_LAMPE = 2119672069U;
        static const AkUniqueID PLAY_POSERFEUILLE = 3817844813U;
        static const AkUniqueID PLAY_PRENDREFEUILLE = 2702842750U;
        static const AkUniqueID PLAY_RANDOMFLOORROOM1 = 43194319U;
        static const AkUniqueID PLAY_RANDOMFLOORROOM2 = 43194316U;
        static const AkUniqueID PLAY_STOREFERME = 3535750338U;
        static const AkUniqueID PLAY_STOREOUVRE = 3670131286U;
        static const AkUniqueID PLAY_TV = 2027969872U;
        static const AkUniqueID PLAY_VENTILATEUR = 3013956005U;
        static const AkUniqueID PLAYER_MOVE = 2248092158U;
        static const AkUniqueID PLAYER_STOP = 3361170585U;
        static const AkUniqueID STARTMUSIC = 3827058668U;
        static const AkUniqueID STOP_ALL = 452547817U;
        static const AkUniqueID STOP_TV = 2893389474U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GROUNDTYPE
        {
            static const AkUniqueID GROUP = 2835351336U;

            namespace STATE
            {
                static const AkUniqueID PARQUET = 1791601991U;
                static const AkUniqueID TAPIS = 1391533176U;
            } // namespace STATE
        } // namespace GROUNDTYPE

        namespace INMENU
        {
            static const AkUniqueID GROUP = 3374585465U;

            namespace STATE
            {
                static const AkUniqueID NO = 1668749452U;
                static const AkUniqueID YES = 979470758U;
            } // namespace STATE
        } // namespace INMENU

        namespace NAVIGATION
        {
            static const AkUniqueID GROUP = 1082482811U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
            } // namespace STATE
        } // namespace NAVIGATION

    } // namespace STATES

    namespace SWITCHES
    {
        namespace VIRAGE_SWITCH
        {
            static const AkUniqueID GROUP = 2654804740U;

            namespace SWITCH
            {
                static const AkUniqueID OFF = 930712164U;
                static const AkUniqueID ON = 1651971902U;
            } // namespace SWITCH
        } // namespace VIRAGE_SWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID ACCELERATION_ROT_RTPC = 3898180431U;
        static const AkUniqueID DISTANCEPIECE = 155651270U;
        static const AkUniqueID DISTANCEVENTILATEUR = 1225552155U;
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
        static const AkUniqueID TVSIDECHAIN = 3411313193U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID NEW_TRIGGER = 4163741908U;
        static const AkUniqueID TRIGGER_STIN_ARMOIRE = 2416262786U;
        static const AkUniqueID TRIGGER_STIN_STORE = 1181401788U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINSOUNDBANK = 534561221U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENT = 77978275U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID SFXAURO = 368632229U;
        static const AkUniqueID TV_EMIT = 3108572563U;
        static const AkUniqueID TV_STATIC = 1436086644U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REVERB = 348963605U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
