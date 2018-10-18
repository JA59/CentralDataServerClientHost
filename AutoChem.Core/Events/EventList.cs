/*
**
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2009 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AutoChem.Core.Arrays;

namespace AutoChem.Core.Events
{
    /// <summary>
    /// A list of events for an observer.
    /// </summary>
    [DataContract]
    public abstract class EventList<TEventKey, TObserver> : IEventList where TObserver : class
    {
        /// <summary>
        /// The actual list of events.  This should not be used and is only public in Silverlight because of the DataContract serialization rules.
        /// </summary>
        [DataMember]
        public readonly List<EventEntry<TEventKey>> m_Events;

        /// <summary>
        /// The collection of handlers for each event type which is initialized by the derived class.
        /// </summary>
        [NonSerialized]
        protected Dictionary<TEventKey, Delegate> m_EventActions;

        /// <summary>
        /// Creates a new Event list.
        /// </summary>
        public EventList()
        {
            m_Events = new List<EventEntry<TEventKey>>();
        }

        /// <summary>
        /// Returns the time of the last get events call.
        /// </summary>
        public DateTime LastGetEventsCall {get; private set;}

        /// <summary>
        /// Returns the time since the last get events call.
        /// </summary>
        public TimeSpan ElapsedTimeSinceLastGetEventsCall
        {
            get { return DateTime.UtcNow - LastGetEventsCall; }
        }

        /// <summary>
        /// Gets the number of outstanding events.
        /// </summary>
        public int EventCount
        {
            get { return m_Events.Count; }
        }

        /// <summary>
        /// Creates the events handlers in m_EventActions in the derived class.
        /// </summary>
        protected abstract void CreateEventActions();

        /// <summary>
        /// Adds an entry to the event list.
        /// </summary>
        protected void AddEventEntry(TEventKey type, params object[] args)
        {
            lock (m_Events)
            {
                m_Events.Add(new EventEntry<TEventKey>(type, args));
            }
        }

        /// <summary>
        /// Gets a copy of the all the events that have not been gotten yet and returns it.  All of these events are removed from this instance of the collection.
        /// </summary>
        protected T GetEvents<T>() where T : EventList<TEventKey, TObserver>, new()
        {
            T eventsT = new T();

            EventList<TEventKey, TObserver> events = eventsT;

            PopulateEvents(events);

            return eventsT;
        }

        private void PopulateEvents(EventList<TEventKey, TObserver> events)
        {
            lock (m_Events)
            {
                // Get all the outstanding events and clear the list.
                events.m_Events.AddRange(m_Events);
                m_Events.Clear();
            }

            LastGetEventsCall = DateTime.UtcNow;
        }

        T IEventList.GetEvents<T>()
        {
            T eventsT = new T();
            object eventsO = eventsT; // Make the compiler happy about casting to EventList<TEventKey, TObserver>

            EventList<TEventKey, TObserver> events = (EventList<TEventKey, TObserver>)eventsO;

            PopulateEvents(events);

            return eventsT;
        }

        /// <summary>
        /// Calls the methods on the observer for the events that have occurred.
        /// </summary>
        public void ProcessEvents(TObserver eventClient)
        {
            if (m_EventActions == null)
            {
                m_EventActions = new Dictionary<TEventKey, Delegate>();
                CreateEventActions();
            }

            foreach (EventEntry<TEventKey> entry in m_Events)
            {
                Delegate deleg = m_EventActions[entry.EventType];

                object[] fullArgs = ArrayUtility.MergeArrays(new object[] { eventClient }, entry.Args);
                deleg.DynamicInvoke(fullArgs);
            }
        }
    }
}

