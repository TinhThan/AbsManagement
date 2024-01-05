export const LayerSpacePanned = {
    id: 'space-panned',
    type: 'circle',
    source: 'spacePanneds',
    filter: ['has', 'point_count'],
    layout:{
        
        'visibility': 'visible'
    },
    paint: {
        'circle-color': [
            'step',
            ['get', 'point_count'],
            '#51bbd6',
            100,
            '#f1f075',
            750,
            '#f28cb1'
        ],
        'circle-radius': [
            'step',
            ['get', 'point_count'],
            20,
            100,
            30,
            750,
            40
        ]
    }
}

export const LayerSpacePannedPoint = {
    id: "space-panned-point",
    type: "circle",
    source: "spacePanneds",
    filter: ["!", ["has", "point_count"]], // Filter out clustered points
    paint: {
        "circle-color": "#11b4da",
        "circle-radius": 15,
        "circle-stroke-width": 1,
        "circle-stroke-color": "#fff"
    },
    layout:{
        
        'visibility': 'visible'
    },
}

export const LayerSpacePannedLabel = {
    id: 'space-panned-label',
    type: 'symbol',
    source: "spacePanneds",
    filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
    layout: {
        'text-field': 'QC', // This is the text that will be displayed
        'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
        'text-size': 12, // Set the text size
        'visibility': 'visible'
    },
    paint: {
        'text-color': '#ffffff', // Set the text color
    }
}
//Handler space not panned

export const LayerSpaceNotPanned = {
    id: 'space-not-panned',
    type: 'circle',
    source: 'spaceNotPanneds',
    filter: ['has', 'point_count'],
    layout:{
        
        'visibility': 'visible'
    },
    paint: {
        'circle-color': [
            'step',
            ['get', 'point_count'],
            '#dbc81a',
            100,
            '#cccc97',
            750,
            '#cccc97'
        ],
        'circle-radius': [
            'step',
            ['get', 'point_count'],
            20,
            100,
            30,
            750,
            40
        ]
    }
}
export const LayerSpaceNotPannedPoint = {
    id: "space-not-panned-point",
    type: "circle",
    source: "spaceNotPanneds",
    filter: ["!", ["has", "point_count"]], // Filter out clustered points
    paint: {
        "circle-color": "#abad15",
        "circle-radius": 15,
        "circle-stroke-width": 1,
        "circle-stroke-color": "#fff"
    },
    layout:{
        
        'visibility': 'visible'
    },
}

export const LayerSpaceNotPannedLabel = {
    id: 'space-not-panned-label',
    type: 'symbol',
    source: "spaceNotPanneds",
    filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
    layout: {
        'text-field': 'QC', // This is the text that will be displayed
        'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
        'text-size': 12, // Set the text size
        'visibility': 'visible'
    },
    paint: {
        'text-color': '#ffffff', // Set the text colo
    }
}

//Handle report
export const cluterLayersReport = {
    id: 'clusters-report',
    type: 'circle',
    source: 'reports',
    filter: ['has', 'point_count'],
    paint: {
        'circle-color': [
            'step',
            ['get', 'point_count'],
            '#51bbd6',
            100,
            '#f1f075',
            750,
            '#f28cb1'
        ],
        'circle-radius': [
            'step',
            ['get', 'point_count'],
            20,
            100,
            30,
            750,
            40
        ],
        'visibility': 'visible'
    }
}

export const unclusteredLayerReport = {
    id: "unclustered-report-point",
    type: "circle",
    source: "reports",
    filter: ["!", ["has", "point_count"]], // Filter out clustered points
    paint: {
        "circle-color": "#dc143c",
        "circle-radius": 15,
        "circle-stroke-width": 1,
        "circle-stroke-color": "#fff",
        'visibility': 'visible'
    },
}

export const unclusteredLabelLayerReport = {
    id: 'unclustered-report-point-label',
    type: 'symbol',
    source: "reports",
    filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
    layout: {
        'text-field': 'BC', // This is the text that will be displayed
        'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
        'text-size': 12, // Set the text size
        'visibility': 'visible'
    },
    paint: {
        'text-color': '#ffffff', // Set the text color
    }
}

