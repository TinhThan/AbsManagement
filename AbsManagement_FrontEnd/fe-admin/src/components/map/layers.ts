export const cluterLayers:any = {
    id: 'clusters',
    type: 'circle',
    source: 'points',
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
        ]
    }
}

export const unclusteredLayer:any = {
    id: "unclustered-point",
    type: "circle",
    source: "points",
    filter: ["!", ["has", "point_count"]], // Filter out clustered points
    paint: {
        "circle-color": "#11b4da",
        "circle-radius": 15,
        "circle-stroke-width": 1,
        "circle-stroke-color": "#fff",
    },
}

export const unclusteredLabelLayer:any = {
    id: 'unclustered-point-label',
    type: 'symbol',
    source: "points",
    filter: ['!', ['has', 'point_count']], // Use the same filter as your circle layer
    layout: {
        'text-field': 'QC', // This is the text that will be displayed
        'text-font': ['Open Sans Semibold', 'Arial Unicode MS Bold'], // Set the text font
        'text-size': 12 // Set the text size
    },
    paint: {
        'text-color': '#ffffff' // Set the text color
    }
    }

